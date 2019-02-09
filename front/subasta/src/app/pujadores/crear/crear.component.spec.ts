import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CrearPujadorComponent } from './crear.component';

describe('CrearComponent', () => {
  let component: CrearPujadorComponent;
  let fixture: ComponentFixture<CrearPujadorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CrearPujadorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CrearPujadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
