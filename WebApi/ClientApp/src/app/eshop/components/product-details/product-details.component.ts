import { Component, OnInit } from '@angular/core';
import { faCartShopping, faStar, faStarHalf } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  faStar = faStar;
  faStarHalf = faStarHalf;
  faCart = faCartShopping;
}
