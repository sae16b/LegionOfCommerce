import {
  Directive,
  ViewContainerRef,
  HostListener,
  ViewChild
} from '@angular/core';
import { DropDownComponent } from '@app/shared/components/drop-down/drop-down.component';

@Directive({
  selector: '[appTooltip]'
})
export class TooltipDirective {
  hovering = false;

  constructor(view: ViewContainerRef) {}

  @HostListener('mouseenter', ['$event']) onMouseEnter($event) {
    this.hovering = true;
  }
  @HostListener('mouseleave', ['$event']) onMouseLeave($event) {
    this.hovering = false;
  }
}
