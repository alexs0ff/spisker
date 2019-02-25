export class RouterStub {

    lastUrl: string;

    navigateByUrl(url: string) {
        this.lastUrl = url;
        return url;
    }
}