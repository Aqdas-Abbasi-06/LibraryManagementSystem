import { Routes } from '@angular/router';
import { Books } from './components/books/books';
import { Students } from './components/students/students';
import { BookIssues } from './components/book-issues/book-issues';

export const routes: Routes = [
  { path: '', redirectTo: 'books', pathMatch: 'full' },
  { path: 'books', component: Books },
  { path: 'students', component: Students },
  { path: 'book-issues', component: BookIssues },
];