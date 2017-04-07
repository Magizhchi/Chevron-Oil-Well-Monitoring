import { Headers } from "@angular/http";

export class HttpHeaders {
    public static jsonPost = new Headers({ 'Content-Type': 'application/json' });
}