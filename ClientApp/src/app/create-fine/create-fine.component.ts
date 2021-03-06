import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../services/http.service'

@Component({
  selector: 'app-finecreate',
  templateUrl: './create-fine.component.html'
})
export class FineCreateComponent implements OnInit {
 
  public finesData: FinesInputViewModel = new FinesInputViewModel(true, 0, new Date(), "", "");
  public id: number;
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
   Create() {
    // await this.http.postPerson(this.personData).subscribe((data: Person) => this.personData = data);
    //await this.http.addCarToPerson(this.carData, this.id).subscribe((data => this.http.getPersonsDataFromServer()));
   // await this.http.addFineToPerson(this.fineData, this.id).subscribe((data) => this.http.getPersonsDataFromServer());
     this.http.addFinesToPerson(this.finesData, this.id).subscribe((data) => this.http.getPersonsDataFromServer());
    location.pathname = '/';

  }
}

interface PersonData {
  
    id: number;
    surname: string;
    city: string;
    address: string;
  //  PersonCars: any;
}
export class Person {
  constructor(public id: number, public surname: string, public city: string, public address: string) {

  }

}
export class CarDataInput {
  constructor(public name: string, public number: string, public date: Date) {

  }
}
export class FineDataInput {
  constructor(public value: number, public number: string, public date: Date, public numberRegistrator:string) {

  }
}
export class FinesInputViewModel {
  constructor(public isPersonal: boolean, public value: number, public dateTime: Date, public numberRegistrator: string,
    public carNumber: string
  ) {

  }

  
}
