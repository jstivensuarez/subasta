import { CurrencyMaskConfig } from "ng2-currency-mask/src/currency-mask.config";
export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
    align: "left",
    allowNegative: false,
    decimal: ",",
    precision: 0,
    prefix: "",
    suffix: "",
    thousands: "."
};

export const CustomWeightMaskConfig: CurrencyMaskConfig = {
    align: "left",
    allowNegative: false,
    decimal: ",",
    precision: 0,
    prefix: "Kg ",
    suffix: "",
    thousands: "."
};