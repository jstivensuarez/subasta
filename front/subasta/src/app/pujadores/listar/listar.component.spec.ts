import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarPujadorComponent } from './listar.component';

describe('ListarComponent', () => {
  let component: ListarPujadorComponent;
  let fixture: ComponentFixture<ListarPujadorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarPujadorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarPujadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
