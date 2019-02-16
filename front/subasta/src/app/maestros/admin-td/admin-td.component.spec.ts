import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTdComponent } from './admin-td.component';

describe('AdminTdComponent', () => {
  let component: AdminTdComponent;
  let fixture: ComponentFixture<AdminTdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminTdComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminTdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
