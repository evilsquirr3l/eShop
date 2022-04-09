import { Category } from "./category";
import { BaseType } from "./baseType";
import { Discount } from "./discount";

export interface Product extends BaseType {
  name: string,
  description: string,
  picruteUrl: string,
  quantity: number,
  category: Category,
  categoryId: number,
  price: number,
  discount: Discount,
  discountId: number
}
