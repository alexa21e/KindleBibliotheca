import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Pagination } from "../models/pagination";
import { Book } from "../models/book";
import { Series } from "../models/series";
import { Author } from "../models/author";
import { BiblParams } from "../models/biblParams";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class BibliothecaService {
    baseUrl = 'https://localhost:5001/api'
    constructor(private http: HttpClient) {
    }

    getBooks(biblParams: BiblParams) {
        let params = new HttpParams();

        if (biblParams.seriesId) params = params.append('seriesId', biblParams.seriesId);
        if (biblParams.authorId) params = params.append('authorId', biblParams.authorId);
        params = params.append('sort', biblParams.sort);
        params = params.append('pageIndex', biblParams.pageIndex);
        params = params.append('pageSize', biblParams.pageSize);
        if(biblParams.search) params = params.append('search', biblParams.search);

        return this.http.get<Pagination<Book[]>>(this.baseUrl + '/books', { params });
    }

    getBook(id: string){
        return this.http.get<Book>(this.baseUrl + '/books/' + id);
    }

    getSeries() {
        return this.http.get<Series[]>(this.baseUrl + '/series');
    }

    getAuthors() {
        return this.http.get<Author[]>(this.baseUrl + '/authors');
    }

    createBook(values: any){
        return this.http.post<Book>(this.baseUrl + '/books/new', values);
    }

    uploadPDF(id: string, formData: FormData): Observable<any>{
        return this.http.post(`${this.baseUrl}/upload/pdf/${id}`, formData);
    }

    downloadPDF(id: string): Observable<Blob>{
        return this.http.get(`${this.baseUrl}/download/pdf/${id}`, { responseType: 'blob' });
    }
}