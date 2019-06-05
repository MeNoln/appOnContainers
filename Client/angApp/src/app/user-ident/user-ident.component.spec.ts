import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserIdentComponent } from './user-ident.component';

describe('UserIdentComponent', () => {
  let component: UserIdentComponent;
  let fixture: ComponentFixture<UserIdentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserIdentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserIdentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
