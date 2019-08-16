import {
  Component,
  ElementRef,
  ViewChild,
  Input,
  HostListener,
  AfterViewInit,
  ViewChildren,
  ContentChildren
} from '@angular/core';

@Component({
  selector: 'app-drop-down',
  templateUrl: './drop-down.component.html',
  styleUrls: ['./drop-down.component.scss']
})
export class DropDownComponent implements AfterViewInit {
  @Input() buttonToOpen: HTMLElement;
  @ViewChild('dropdown', { static: false }) dropdown: ElementRef;

  dropdownVisible = false;
  constructor() {}

  // Clicking outside of dropdown causes it to close
  @HostListener('document:click', ['$event'])
  clickDocument(event) {
    const target = event.target as HTMLElement;
    if (this.dropdownVisible && target !== this.buttonToOpen) {
      if (!this.dropdown.nativeElement.contains(event.target)) {
        this.dropdownVisible = false;
      } else {
        if (event.target && event.target.classList.contains('close-on-click')) {
          this.dropdownVisible = false;
        }
      }
    }
  }

  // Toggle open dropdown
  ngAfterViewInit() {
    this.buttonToOpen.onclick = () => {
      this.dropdownVisible = !this.dropdownVisible;
    };
  }
}
