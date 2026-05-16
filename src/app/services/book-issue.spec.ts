import { TestBed } from '@angular/core/testing';

import { BookIssue } from './book-issue';

describe('BookIssue', () => {
  let service: BookIssue;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookIssue);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
