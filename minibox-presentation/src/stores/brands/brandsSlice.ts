import { createSlice } from "@reduxjs/toolkit";
import actGetBrands from "./act/actGetBrands";
import { TLoading } from "@customtypes/shared";
import { TBrand } from "@customtypes/brand";

interface ITBrandsState {
  records: TBrand[];
  loading: TLoading;
  error: string | null;
}

const initialState: ITBrandsState = {
  records: [],
  loading: "idle",
  error: null,
};

const brandsSlice = createSlice({
  name: "brands",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(actGetBrands.pending, (state) => {
      state.loading = "pending";
      state.error = null;
    });
    builder.addCase(actGetBrands.fulfilled, (state, action) => {
      state.loading = "succeeded";
      state.records = action.payload;
    });
    builder.addCase(actGetBrands.rejected, (state, action) => {
      state.loading = "failed";
      if (action.payload && typeof action.payload === "string") {
        state.error = action.payload;
      }
    });
  },
});

export { actGetBrands };
export default brandsSlice.reducer;