import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "@stores/hooks";
import { actGetProductByBanner } from "@stores/product/productsSlice";
import { TProduct } from "@customtypes/product";
import { StartRating, LoadingCard } from "@components/index";
import { useNavigate } from "react-router-dom";

const ProductSession = ({ para }: { para: string }) => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { records: productsBestSeller } = useAppSelector(
    (state) => state.products
  );

  useEffect(() => {
    dispatch(actGetProductByBanner(para));
  }, [dispatch, para]);

  const toArray = (num: number) => {
    let arr = [];
    for (var i = 1; i <= num; i++) {
      arr.push(i);
    }
    return arr;
  };

  return (
    <div className="mt-6 grid grid-cols-2 gap-x-4 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
      {productsBestSeller.length > 0 ? (
        productsBestSeller.map((item: TProduct) => (
          <div key={item.id} className="group relative">
            <div className="w-full overflow-clip relative">
              <img
                src={item.img}
                alt="primary image"
                className="aspect-square w-full rounded-md bg-gray-200 object-cover lg:aspect-auto lg:h-80 transition-all group-hover:opacity-0"
              />
              <img
                src={item.img2}
                alt="secondary image"
                className="transition-all ease-out inset-0 opacity-0 absolute h-full w-full group-hover:opacity-100"
              />
            </div>

            <div className="mt-4 flex flex-col sm:flex-row lg:flex-row justify-between">
              <div className="overflow-hidden">
                <h3 className="text-base font-semibold sm:text-lg lg:text-xl">
                  <a href="/product/{id}">
                    <span aria-hidden="true" className="break-all "></span>
                    {item.title}
                  </a>
                </h3>
              </div>
              <div className="px-2">
                {item.oldPrice == undefined || (
                  <p className="text-sm font-medium text-gray-900 line-through">
                    ${item.oldPrice.toFixed(2)}
                  </p>
                )}
                <p className="text-sm font-medium text-gray-900">
                  ${item.price.toFixed(2)}
                </p>
              </div>
            </div>
            <div className="py-1 h-10 overflow-hidden">
              <p className="mt-1 text-xs text-gray-500 break-all">
                {item.summary}
              </p>
            </div>
            {item.rating == undefined || (
              <div className="flex items-center justify-center">
                {toArray(item.rating).map((i) => (
                  <StartRating key={i} classpro="w-4 h-4 text-primary ms-1" />
                ))}
                {toArray(5 - item.rating).map((i) => (
                  <StartRating
                    key={i}
                    classpro="w-4 h-4 ms-1 text-gray-300 dark:text-gray-500 opacity-50"
                  />
                ))}
              </div>
            )}

            {item.discount == undefined ||
              (item.discount > 0 ? (
                <div className="absolute inset-0 bg-primary rounded-sm w-fit h-fit m-1 p-1 sm:m-2 sm:p-2 lg:m-2 lg:p-2 lg:size-fit text-center content-center text-white font-semibold text-xs lg:text-base">
                  {`- ${item.discount}%`}
                </div>
              ) : (
                <></>
              ))}
          </div>
        ))
      ) : (
        <div className="col-span-full">
          <LoadingCard />
        </div>
      )}
      <div className="col-span-full text-center">
        <button
          className="border-1 border-primary-brightness px-6 py-2 rounded-sm font-medium text-xs lg:text-base cursor-pointer hover:bg-primary-brightness"
          onClick={() => navigate("/")}
        >
          Shop All Product
        </button>
      </div>
    </div>
  );
};

export default ProductSession;
