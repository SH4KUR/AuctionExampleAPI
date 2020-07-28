import { SignalRService } from './../signal-r.service';
import { RatesService } from './../rates.service';
import { Component, OnInit } from '@angular/core';

import { ItemsService } from './../items.service';
import { Item } from "./../item";
import { Rate } from "./../rate";

import { ActivatedRoute } from '@angular/router';
import { Location } from "@angular/common";

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {

  item: Item;
  rates: Rate[] = [];

  ratePrice: number;
  userName: string;

  constructor(
    private itemsService: ItemsService,
    private ratesService: RatesService, 
    private route: ActivatedRoute,
    private location: Location,
    private signalRService: SignalRService) {
      this.getItem();
    }

  ngOnInit(): void {
    this.signalRService.refreshListener(() => { this.refresh(); });
    
    this.getRates();
  }

  getItem(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.itemsService.getItem(id)
      .subscribe(item => this.item = item);
  }

  getRates(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.ratesService.getRatesByItem(id)
      .subscribe(rates => this.rates = rates.sort(this.compare));
  }

  getUserName(): void {
    this.userName = (document.getElementById('userName') as HTMLInputElement).value;
  }

  goBack(): void {
    this.location.back();
  }

  sendRate(): void {
    this.getUserName();
    
    let newRate: Rate = {
      rateId: 0,
      itemId: this.item.itemId,
      price: this.ratePrice,
      rateTime: new Date(Date.now()),
      userName: this.userName
    };

    this.ratesService.addRate(newRate)
      .subscribe(rate => console.log(`Added Rate ID:${ rate.rateId }`));
  }

  refresh(): void {
    this.getItem();
    this.getRates();
  }

  compare(r1: Rate, r2: Rate) {
    if(r1.rateId < r2.rateId) return 1;
    if(r1.rateId > r2.rateId) return -1;
    return 0;
  }

}
