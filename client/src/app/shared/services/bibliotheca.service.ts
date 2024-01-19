import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Pagination } from "../models/pagination";
import { Book } from "../models/book";

@Injectable({
    providedIn: 'root' 
})

export class BibliothecaService{
    baseUrl = 'https://localhost:5001/api/'
    constructor(private http: HttpClient){
    }

    getBooks(){
        return this.http.get<Pagination<Book[]>>(this.baseUrl + 'books?pageSize=50');
    }
}