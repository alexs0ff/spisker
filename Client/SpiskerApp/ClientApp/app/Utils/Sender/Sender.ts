import { Injectable } from '@angular/core';
//import { Http, Response, Request, Headers, RequestMethod, URLSearchParams } from '@angular/http';
import { HttpClient, HttpHandler, HttpHeaders, HttpParams, HttpRequest, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, Observer } from "rxjs/rx";
import { OAuth2Service } from "../../OAuth2/OAuth2Module";
import {ErrorFlowService} from "../../ErrorFlow/ErrorFlowModule";
import { StringExtentions } from "../Extentions/StringExtentions";

class RequestMethod {
    static Get:string = "GET";
    static Post:string = "POST";
    static Put:string = "PUT";
    static Delete:string = "DELETE";
    static Options:string = "OPTIONS";
    static Head: string = "HEAD";
    static Patch:string = "PATCH";
} 

@Injectable() 
export class Sender {
     /* prod**     
    private baseUrl: string = "http://api.spisker.ru";
         **end-prod */
    /* dev */
    private baseUrl: string = "http://localhost:9623";
    /* end-dev */

    private contentTypeJsonName: string = 'Content-Type';

    private cacheControlName: string = 'Cache-Control';

    private contentTypeJsonValue: string = 'application/json';

    constructor(private http: HttpClient, private oauth2Service: OAuth2Service, private errorFlowService: ErrorFlowService) {
        
    }

    private combineUrl(url:string): string {
        if (!url || url.length===0) {
            return this.baseUrl;
        }
        
        if (url[0] === '/' || url[0] === '\\') {
            return this.baseUrl + url;
        }

        return this.baseUrl+'/' + url;
    }

    getJson(data: any, url: string): Observable<any> {
        return this.get(data, url, null);
    }

    getAuthJson(data: any, url: string): Observable<any> {
        var headers = new HttpHeaders();
        headers = headers.append("Authorization", this.oauth2Service.getTokenValue());
        return this.get(data, url, headers);
    }

    getTryAuthJson(data: any, url: string): Observable<any> {
        let token = this.oauth2Service.getTokenValue();

        if (!StringExtentions.isNullOrWhiteSpace(token)) {
            return this.getAuthJson(data, url);
        }
        return this.getJson(data,url);
    }

    get(data: any, url: string, headers: HttpHeaders): Observable<Object> {
        return this.send(data, url, headers, RequestMethod.Get);
    }

    deleteJson(data: any, url: string): Observable<any> {
        return this.sendJsonWithHeaders(data, url, null, RequestMethod.Delete);
    }

    deleteAuthJson(data: any, url: string): Observable<any> {
        return this.sendAuthJson(data, url, RequestMethod.Delete);
    }

    putJson(data: any, url: string): Observable<any> {
        return this.sendJsonWithHeaders(data, url, null, RequestMethod.Put);
    }

    putAuthJson(data: any, url: string): Observable<any> {
        return this.sendAuthJson(data, url, RequestMethod.Put);
    }

    postJson(data: any, url: string): Observable<any> {
        return this.sendJsonWithHeaders(data, url, null,RequestMethod.Post);
    }

    postFileAuthJson(file: File, url: string): Observable<any> {
        let formData: FormData = new FormData();
        formData.append('UploadFile', file, file.name);
        return this.sendAuthJson(formData, url, RequestMethod.Post);
    }

    postAuthJson(data: any, url: string): Observable<any> {
        return this.sendAuthJson(data, url, RequestMethod.Post);
    }

    sendAuthJson(data: any, url: string, httpMethod: string): Observable<any> {
        var headers = new HttpHeaders();
        headers = headers.append("Authorization", this.oauth2Service.getTokenValue());

        return this.sendJsonWithHeaders(data, url, headers, httpMethod);
    }

    sendJsonWithHeaders(data: any, url: string, headers: HttpHeaders, httpMethod: string): Observable<any> {
        
        if (headers == null) {
            headers = new HttpHeaders();
        }

        let dataToSend: any;

        if (data instanceof FormData) {
            dataToSend = data;
        } else {
            if (!headers.has(this.contentTypeJsonName)) {
                headers = headers.append(this.contentTypeJsonName, this.contentTypeJsonValue);
            }
            dataToSend = JSON.stringify(data);
        }

        return this.send(dataToSend, url, headers, httpMethod);
    }
    

    postAuth(username: string, password: string, url: string): Observable<any> {
        var headers = new HttpHeaders({
            'Content-Type': 'application/x-www-form-urlencoded'
        });

        var urlSearchParams = new HttpParams();
        urlSearchParams = urlSearchParams.append('grant_type', "password");
        urlSearchParams = urlSearchParams.append('username', username);
        urlSearchParams = urlSearchParams.append('password', password);
        var body = urlSearchParams.toString();
        return this.send(body, url, headers, RequestMethod.Post);
    }

    //Отправка и принятие данных.
    send(data: any, url: string, headers: HttpHeaders, httpMethod: string): Observable<Object> {
        let toUrl = this.combineUrl(url);

        let searchParams: HttpParams = null;

        if (httpMethod === RequestMethod.Get) {
            searchParams = this.objToSearchParams(data);
            searchParams = searchParams.append('timestamp', new Date().getTime().toString());
            data = null;
        }

        if (headers==null || headers===undefined) {
            headers = new HttpHeaders();
        }

        if (headers.has(this.cacheControlName)) {
            headers = headers.append(this.cacheControlName, "no-cache");
        }

        let flow: Observable<Object>;


        flow = this.http.request(httpMethod,
            toUrl,
            {
                body: data,
                headers: headers,
                params: searchParams
            }
        );

        var response = flow.do(null, (error: HttpErrorResponse) => {
            let message: string;
            if (error.error instanceof Error) {
                // A client-side or network error occurred. Handle it accordingly.
                message = error.error.message;
            } else {
                message = error.error;
            }
            this.errorFlowService.registerCommunicationError(error.status, error.statusText, message);
        });
        //Using share is overkill. if you just want to process the error inline, you can use do instead:
        //.do(null, err => this.appErrorsService.addApiError(err));
        return response;
    }

    private objToSearchParams(obj: any): HttpParams {
        let params: HttpParams = new HttpParams();
        for (var key in obj) {
            if (obj.hasOwnProperty(key))
                params = params.set(key, obj[key]);
        }
        return params;
    }
}