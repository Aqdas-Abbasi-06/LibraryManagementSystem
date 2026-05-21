import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BookService } from '../../services/book';

@Component({
  selector: 'app-books',
  imports: [FormsModule, CommonModule],
  templateUrl: './books.html',
  styleUrl: './books.css',
})
export class Books implements OnInit {
  books: any[] = [];

  newBook = {
    title: '',
    author: '',
    isbn: '',
    category: '',
    genre: '',
    shelfNumber: ''
  };

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getBooks().subscribe((data: any) => {
      this.books = data;
    });
  }

  addBook() {
    if (this.newBook.title && this.newBook.author) {
      this.bookService.addBook(this.newBook).subscribe(() => {
        this.loadBooks();
        this.newBook = { title: '', author: '', isbn: '', category: '', genre: '', shelfNumber: '' };
      });
    }
  }

  deleteBook(id: number) {
    this.bookService.deleteBook(id).subscribe(() => {
      this.loadBooks();
    });
  }
}