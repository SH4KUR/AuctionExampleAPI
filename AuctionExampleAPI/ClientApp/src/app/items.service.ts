import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";

import { Item } from './item';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  private itemsUrl = '/api/items';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getItems() : Observable<Item[]> {
    return this.http.get<Item[]>(this.itemsUrl)
    .pipe(
      tap(_ => console.log('Fetched Items')),
      catchError(this.handleError<Item[]>('getItems', []))
    );
  }

  getItem(id: number) : Observable<Item> {
    return this.http.get<Item>(`${this.itemsUrl}/${ id }`)
    .pipe(
      tap(_ => console.log(`Fetched Item, ID: ${ id }`)),
      catchError(this.handleError<Item>(`getItem, id=${ id }`))
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
