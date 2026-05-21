import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-students',
  imports: [FormsModule, CommonModule],
  templateUrl: './students.html',
  styleUrl: './students.css',
})
export class Students {
  students: any[] = [];

  newStudent = {
    name: '',
    email: '',
    phone: '',
    address: ''
  };

  addStudent() {
    if (this.newStudent.name && this.newStudent.email) {
      this.students.push({ ...this.newStudent, id: Date.now() });
      this.newStudent = { name: '', email: '', phone: '', address: '' };
    }
  }

  deleteStudent(id: number) {
    this.students = this.students.filter(student => student.id !== id);
  }
}