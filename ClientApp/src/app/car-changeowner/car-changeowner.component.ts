import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service';
import { ActivatedRoute, Data, Router } from '@angular/router';
import { Time } from '@angular/common';

@Component({
  selector: 'app-car-changeowner',
  templateUrl: './car-changeowner.component.html'
})
export class ChangeCarOwnerComponent implements OnInit {

  public id: number;
  public changeData: ChangeCarOwnerData = new ChangeCarOwnerData(0, new Date());
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //  }
  constructor(private http: HttpService, private route: ActivatedRoute) {

    }
    ngOnInit(): void {

      this.id = +this.route.snapshot.paramMap.get('id');

      //  this.http.getPersonsDataFromServer().subscribe((data: PersonData[]) => this.personsData = data);
  }
  async Save() {
    //await this.http.postPerson(this.personData).subscribe((data: Person) => this.personData = data);
    await this.http.changeOwner(this.id, this.changeData).subscribe((data: ChangeCarOwnerData) => this.http.getPersonsDataFromServer);
    
    
    location.pathname = '/';

  }
}


export class ChangeCarOwnerData {
  constructor(public id: number, public date: Date) {

  }
}
