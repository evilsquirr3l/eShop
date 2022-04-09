import { BaseType } from "./baseType";

export interface Discount extends BaseType {
  name: string,
  description: string,
  discountPercent: number,
  isActive: boolean
}
