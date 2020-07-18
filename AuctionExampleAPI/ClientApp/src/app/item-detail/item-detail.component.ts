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

  constructor(
    private itemsService: ItemsService,
    private ratesService: RatesService, 
    private route: ActivatedRoute,
    private location: Location
    ) { }

  ngOnInit(): void {
    this.getItem();
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
      .subscribe(rates => this.rates = rates);
  }

  goBack(): void {
    this.location.back();
  }

}
