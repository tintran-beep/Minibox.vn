import { createSlice } from "@reduxjs/toolkit";
import actGetProductByBanner from "./act/actGetProductByBanner";
import { TLoading } from "@customtypes/shared";
import { TProduct } from "@customtypes/product";

interface IProductItemState {
  records: TProduct[];
  loading: TLoading;
  error: string | null;
}

const initialState: IProductItemState = {
  records: [],
  loading: "idle",
  error: null,
};

const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {
    productsCleanUp: (state) => {
      state.records = [];
    },
  },
  extraReducers: (builder) => {
    builder.addCase(actGetProductByBanner.pending, (state) => {
      state.loading = "pending";
      state.error = null;
    });
    builder.addCase(actGetProductByBanner.fulfilled, (state, action) => {
      state.loading = "succeeded";
      state.records = action.payload;
    });
    builder.addCase(actGetProductByBanner.rejected, (state, action) => {
      state.loading = "failed";
      if (action.payload && typeof action.payload === "string") {
        state.error = action.payload;
      }
    });
  },
});

export const { productsCleanUp } = productsSlice.actions;
export { actGetProductByBanner };
export default productsSlice.reducer;