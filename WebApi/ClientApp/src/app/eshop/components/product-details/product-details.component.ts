import { Component, OnInit } from '@angular/core';
import { faCartShopping, faStar, faStarHalf } from '@fortawesome/free-solid-svg-icons';
import { Product } from '../../models/product';
import { Category } from '../../models/category';
import { Discount } from '../../models/discount';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  faStar = faStar;
  faStarHalf = faStarHalf;
  faCart = faCartShopping;
  testDate: Date = new Date();
  testDiscount: Discount = {
    createdAt: this.testDate,
    description: 'TestDiscountDescription',
    discountPercent: 10,
    id: 1,
    isActive: false,
    isDeleted: false,
    modifiedAt: this.testDate,
    name: 'DiscountName',
  };
  testCategory: Category = {
    createdAt: this.testDate,
    description: 'Category Description',
    id: 1,
    isDeleted: false,
    modifiedAt: this.testDate,
    name: 'CategoryName',
  };
  testProduct: Product = {
    category: this.testCategory,
    categoryId: this.testCategory.id,
    createdAt: this.testDate,
    description: 'test description',
    discount: this.testDiscount,
    discountId: this.testDiscount.id,
    id: 1,
    isDeleted: false,
    modifiedAt: this.testDate,
    name: 'TestProductName',
    pictureUrl: 'https://m.media-amazon.com/images/I/71D9ImsvEtL._UY500_.jpg',
    price: 228,
    quantity: 5,
  };

  constructor() {
  }

  ngOnInit(): void {
  }
}
