import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Pagination } from "../models/pagination";
import { Book } from "../models/book";
import { Series } from "../models/series";
import { Author } from "../models/author";
import { BiblParams } from "../models/biblParams";

@Injectable({
    providedIn: 'root'
})

export class BibliothecaService {
    baseUrl = 'https://localhost:5001/api/'
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

        return this.http.get<Pagination<Book[]>>(this.baseUrl + 'books', { params });
    }

    getSeries() {
        return this.http.get<Series[]>(this.baseUrl + 'books/series');
    }

    getAuthors() {
        return this.http.get<Author[]>(this.baseUrl + 'books/authors');
    }
}