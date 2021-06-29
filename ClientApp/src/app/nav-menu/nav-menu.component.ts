import { Component } from '@angular/core';
import { HttpService } from '../services/http.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  public dataSearch: string;
  collapse() {
    this.isExpanded = false;
  }
  public Search(data: string) {
    location.pathname = "/search-result/" + this.dataSearch;
    //return this.http.Search(this.dataSearch).subscribe((d) => this.http.getPersonsDataFromServer());
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  constructor(private http: HttpService) {

  }
}
