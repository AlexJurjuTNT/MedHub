import {Component, ElementRef, EventEmitter, Input, NgModule, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {DxTreeViewComponent, DxTreeViewModule, DxTreeViewTypes} from 'devextreme-angular/ui/tree-view';
import {getNavigation} from '../../../app-navigation';

import * as events from 'devextreme/events';
import {TokenService} from "../../services/token.service";

@Component({
  selector: 'app-side-navigation-menu',
  templateUrl: './side-navigation-menu.component.html',
  styleUrls: ['./side-navigation-menu.component.scss']
})
export class SideNavigationMenuComponent implements OnInit, OnDestroy {
  @ViewChild(DxTreeViewComponent, {static: true})
  menu!: DxTreeViewComponent;

  @Output()
  selectedItemChanged = new EventEmitter<DxTreeViewTypes.ItemClickEvent>();

  @Output()
  openMenu = new EventEmitter<any>();

  private _selectedItem!: String;
  private _items: Record<string, unknown>[] = [];
  private _compactMode = false;

  constructor(
    private elementRef: ElementRef,
    private tokenService: TokenService,
  ) {
  }

  ngOnInit() {
    this.updateNavigation();
  }

  updateNavigation() {
    const navItems = getNavigation(this.tokenService);
    this._items = navItems.map((item) => {
      if (item.path && !(/^\//.test(item.path))) {
        item.path = `/${item.path}`;
      }
      return {...item, expanded: !this._compactMode};
    });

    if (this.menu && this.menu.instance) {
      this.menu.instance.option('items', this._items);
    }
  }

  @Input()
  set selectedItem(value: String) {
    this._selectedItem = value;
    if (this.menu && this.menu.instance) {
      this.menu.instance.selectItem(value);
    }
  }

  get items() {
    return this._items;
  }

  @Input()
  get compactMode() {
    return this._compactMode;
  }

  set compactMode(val) {
    this._compactMode = val;

    if (this.menu && this.menu.instance) {
      if (val) {
        this.menu.instance.collapseAll();
      } else {
        this.menu.instance.expandItem(this._selectedItem);
      }
    }
  }

  onItemClick(event: DxTreeViewTypes.ItemClickEvent) {
    this.selectedItemChanged.emit(event);
  }

  ngAfterViewInit() {
    events.on(this.elementRef.nativeElement, 'dxclick', (e: Event) => {
      this.openMenu.next(e);
    });
  }

  ngOnDestroy() {
    events.off(this.elementRef.nativeElement, 'dxclick');
  }
}

@NgModule({
  imports: [DxTreeViewModule],
  declarations: [SideNavigationMenuComponent],
  exports: [SideNavigationMenuComponent]
})
export class SideNavigationMenuModule {
}
