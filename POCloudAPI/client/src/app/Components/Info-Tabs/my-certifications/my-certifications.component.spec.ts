import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyCertificationsComponent } from './my-certifications.component';

describe('MyCertificationsComponent', () => {
  let component: MyCertificationsComponent;
  let fixture: ComponentFixture<MyCertificationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyCertificationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCertificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
