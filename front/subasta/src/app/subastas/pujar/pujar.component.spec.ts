import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PujarComponent } from './pujar.component';

describe('PujarComponent', () => {
  let component: PujarComponent;
  let fixture: ComponentFixture<PujarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PujarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PujarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
