import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../services/http.service';
import { Router } from '@angular/router'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html'
})
export class SearchComponent implements OnInit {
  public personsData: PersonDataOutput[];
  public lst: number[]  = new Array();
  public Checked: boolean[];
  public search: string;
  public searchData: Search = new Search("");
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //    http.get<string[]>(baseUrl + 'api/Persons').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //  }
  constructor(private http: HttpService, private route: ActivatedRoute) {

    }
   async ngOnInit() {

   
     console.log(this.personsData);    
     this.search = this.route.snapshot.paramMap.get('id');
     this.searchData.data = this.route.snapshot.paramMap.get('id');
     console.log(this.search);
     await this.http.Search(this.searchData).subscribe((data: PersonDataOutput[]) => this.personsData = data);
    //  await   this.http.getPersonsDataFromServer().subscribe((data: PersonDataOutput[]) => this.personsData = data);
     // this.Checked.length = this.personsData.length;

  }
  public New() {

  }

  update() {
    
    location.pathname = '/update';
    
  }
  public deleteF() {
   // alert("Alert");

    this.http.deletePerson(this.lst);
    for (var i = 0; i < this.lst.length; i++) {
      this.http.detetePersonById(this.lst[i]).subscribe(data => this.ngOnInit());
    }
 //   this.http.deletePerson(this.lst).subscribe(data => this.ngOnInit());

    console.log(this.personsData);
  }
  Change(v) {
    
    if (!this.lst.includes(v) || this.lst == undefined) {

      this.lst.push(v);
    }
    else {
      var i = this.lst.indexOf(v);
      this.lst.splice(i,1);
    }
   
    
   
    console.log(this.lst);


  };
  CreateNew() {
    location.pathname = '/create';
  }
  CarsInfo() {
    location.pathname = '/cars';
  }
}

interface PersonData {
  
    id: number;
    name: string;
    city: string;
  address: string;
  
 // person: Person;
  //finedata: string;
 // carsdata: string;
  //  PersonCars: any;
}
interface PersonDataOutput {
  id: number;
  surname: string;
  city: string;
  address: string;
  fineData: string;
  carData: string;

}
export class  Search {
  
  constructor(public data: string) {

  }
}
