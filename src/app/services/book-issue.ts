import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BookIssueService {
  private apiUrl = 'http://localhost:5016/api/BookIssues';

  constructor(private http: HttpClient) {}

  getBookIssues() {
    return this.http.get(this.apiUrl);
  }

  addBookIssue(issue: any) {
    return this.http.post(this.apiUrl, issue);
  }

  deleteBookIssue(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  updateBookIssue(id: number, issue: any) {
    return this.http.put(`${this.apiUrl}/${id}`, issue);
  }
}