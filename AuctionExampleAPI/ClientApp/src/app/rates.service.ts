import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";

import { Rate } from "./rate";

@Injectable({
  providedIn: 'root'
})
export class RatesService {

  private itemsUrl = '/api/rates';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getRatesByItem(id: number) : Observable<Rate[]> {
    return this.http.get<Rate[]>(`${ this.itemsUrl }/item/${ id }`)
    .pipe(
      tap(_ => console.log(`Fetched Rate by Item, ID: ${ id }`)),
      catchError(this.handleError<Rate[]>(`getRatesByItem, id=${ id }`))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error)
      console.log(`${ operation } failed: ${ error.message }`);
      return of(result as T);
    };
  }
}
