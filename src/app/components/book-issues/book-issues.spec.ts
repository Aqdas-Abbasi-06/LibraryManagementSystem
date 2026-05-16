import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookIssues } from './book-issues';

describe('BookIssues', () => {
  let component: BookIssues;
  let fixture: ComponentFixture<BookIssues>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookIssues],
    }).compileComponents();

    fixture = TestBed.createComponent(BookIssues);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
