import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarSimpleComponent } from './editar-simple.component';

describe('EditarSimpleComponent', () => {
  let component: EditarSimpleComponent;
  let fixture: ComponentFixture<EditarSimpleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditarSimpleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditarSimpleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
