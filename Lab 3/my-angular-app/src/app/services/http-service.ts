import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Post } from "../models/post";

@Injectable({
    providedIn: 'root'
})
export class HttpService {
    constructor(private readonly _http: HttpClient) { }

    getPosts(): Observable<Post[]> {
        return this._http.get<Post[]>('https://localhost:44380/api/post/all');
    }

    createUser(username: string, password: string): Observable<string> {
        const payload = { Username: username, Password: password };
        return this._http.post<string>('https://localhost:44380/api/authorize/register', payload, { responseType: 'text' as 'json' });
    }

    createPost(title: string, content: string, token: string): Observable<number> {
        const payload = { Title: title, Content: content };
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
        });
        return this._http.post<number>('https://localhost:44380/api/post/create', payload, { headers });
    }
}