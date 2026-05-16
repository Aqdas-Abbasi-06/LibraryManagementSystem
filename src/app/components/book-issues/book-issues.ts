import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-issues',
  imports: [FormsModule, CommonModule],
  templateUrl: './book-issues.html',
  styleUrl: './book-issues.css',
})
export class BookIssues {
  bookIssues: any[] = [];

  newIssue = {
    studentName: '',
    bookTitle: '',
    issueDate: '',
    returnDate: ''
  };

  addIssue() {
    if (this.newIssue.studentName && this.newIssue.bookTitle) {
      this.bookIssues.push({ ...this.newIssue, id: Date.now() });
      this.newIssue = { studentName: '', bookTitle: '', issueDate: '', returnDate: '' };
    }
  }

  deleteIssue(id: number) {
    this.bookIssues = this.bookIssues.filter(issue => issue.id !== id);
  }
}