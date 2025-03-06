import { createSlice } from "@reduxjs/toolkit";
import actGetGeneralPromotion from "./act/actGetGeneralPromotion";
import { TLoading } from "@customtypes/shared";
import { TGeneralPromotion } from "@customtypes/general-promotion";

interface ITPromotionsState {
  records: TGeneralPromotion[];
  loading: TLoading;
  error: string | null;
}

const initialState: ITPromotionsState = {
  records: [],
  loading: "idle",
  error: null,
};

const promotionsSlice = createSlice({
  name: "generalPromotions",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(actGetGeneralPromotion.pending, (state) => {
      state.loading = "pending";
      state.error = null;
    });
    builder.addCase(actGetGeneralPromotion.fulfilled, (state, action) => {
      state.loading = "succeeded";
      state.records = action.payload;
    });
    builder.addCase(actGetGeneralPromotion.rejected, (state, action) => {
      state.loading = "failed";
      if (action.payload && typeof action.payload === "string") {
        state.error = action.payload;
      }
    });
  },
});

export { actGetGeneralPromotion };
export default promotionsSlice.reducer;