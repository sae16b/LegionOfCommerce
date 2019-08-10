import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SocialMediaAuthComponent } from './social-media-auth.component';

describe('SocialMediaAuthComponent', () => {
  let component: SocialMediaAuthComponent;
  let fixture: ComponentFixture<SocialMediaAuthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SocialMediaAuthComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SocialMediaAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
