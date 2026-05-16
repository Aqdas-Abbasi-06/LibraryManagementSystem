import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private apiUrl = 'http://localhost:5016/api/Books';

  constructor(private http: HttpClient) {}

  getBooks() {
    return this.http.get(this.apiUrl);
  }

  addBook(book: any) {
    return this.http.post(this.apiUrl, book);
  }

  deleteBook(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  updateBook(id: number, book: any) {
    return this.http.put(`${this.apiUrl}/${id}`, book);
  }
}
