import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-social-media-auth',
  templateUrl: './social-media-auth.component.html',
  styleUrls: ['./social-media-auth.component.scss']
})
export class SocialMediaAuthComponent implements OnInit {
  @Input() formDisabled: boolean; // This will be determined by encompassing form

  constructor() {}

  ngOnInit() {}
}
