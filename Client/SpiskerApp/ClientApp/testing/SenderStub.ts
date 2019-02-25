import { Observable } from "rxjs/rx";
import "rxjs/add/operator/map";
import "rxjs/add/operator/filter";


export class SenderStub {

    receivedData:any;

    getJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    getAuthJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    getTryAuthJson(data: any, url: string): Observable<any> {
      
        return this.getJson(data, url);
    }

    deleteJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    deleteAuthJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    putJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    putAuthJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    postJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    postFileAuthJson(file: File, url: string): Observable<any> {
        return this.send(file, url);
    }

    postAuthJson(data: any, url: string): Observable<any> {
        return this.send(data, url);
    }

    sendAuthJson(data: any, url: string): Observable<any> {
        return this.send(data, url); 
    }

    sendJsonWithHeaders(data: any, url:any): Observable<any> {

        return this.send(data, url);
    }


    postAuth(username: string, password: string, url: string): Observable<any> {
        let data = {
            userName: username,
            password: password
        }
        return this.send(data, url);
    }

    send(data: any, url: string): Observable<any>  {
        return Observable.create((observer:any) => {
            observer.onNext(this.receivedData);
        });
    }
}