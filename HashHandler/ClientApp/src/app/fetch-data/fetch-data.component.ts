import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {

  public hashesResponse: HashResponse[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<HashesResponse>(baseUrl + 'hashes').subscribe({
      next: response => {
        this.hashesResponse = response.hashResponse;
      },
      error: error => {
        console.error('There was an error!', error);
      }
    })
  }
}

interface HashesResponse {
  hashResponse: [];
}

interface HashResponse {
  date: string;
  count: number;
}

