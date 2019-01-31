import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearLotesComponent } from './crear-lotes.component';

describe('CrearLotesComponent', () => {
  let component: CrearLotesComponent;
  let fixture: ComponentFixture<CrearLotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrearLotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrearLotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
