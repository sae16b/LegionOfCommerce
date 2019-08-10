import { Directive, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appHitEnter]'
})
export class HitEnterDirective {
  @Input() hitEnterFunction: () => void;

  constructor() {}

  @HostListener('keypress.enter') onKeyPress() {
    this.hitEnterFunction();
  }
}
