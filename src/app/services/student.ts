import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private apiUrl ='https://librarymanagementsystem-production-ee1b.up.railway.app/api/Students';

  constructor(private http: HttpClient) {}

  getStudents() {
    return this.http.get(this.apiUrl);
  }

  addStudent(student: any) {
    return this.http.post(this.apiUrl, student);
  }

  deleteStudent(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  updateStudent(id: number, student: any) {
    return this.http.put(`${this.apiUrl}/${id}`, student);
  }
}
