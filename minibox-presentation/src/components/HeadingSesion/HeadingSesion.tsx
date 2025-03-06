import {bgHeading} from "@assets/images";
import { useNavigate } from "react-router-dom";

function HeadingSession() {
  const navigate = useNavigate();
  return (
    <>
      <section>
        <div className="px-3 mx-auto max-w-full">
          <div className="grid items-center grid-cols-1 gap-10 lg:grid-cols-2  bg-gray-100">
            <div className="text-center py-4  order-2 lg:order-1">
              <h3 className="text-xl sm:text-2xl lg:text-2xl pb-6 font-semibold  text-primary">
                The Perfect Skincare
              </h3>
              <h1 className="text-3xl sm:text-4xl lg:text-4xl pb-4 font-roboto font-semibold text-black">
                Care for Your Skin
              </h1>
              <h1 className="text-3xl sm:text-4xl lg:text-4xl pb-4 font-roboto font-semibold text-black">
                Care for Your Beauty
              </h1>
              <p className="text-sm sm:text-base lg:text-xl pb-6 mx-0 text-gray-500">
                Made using clean, non-toxic ingredients, our products are
                designed for everyone.
              </p>
              <button
                type="button"
                className="inline-flex items-center justify-center px-12 py-3 text-sm font-semibold leading-5 text-white transition-all duration-200 bg-black border border-transparent rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-900 hover:bg-primary-brightness cursor-pointer"
                onClick={()=>navigate('categories')}
              >
                Shop Now
              </button>
            </div>

            <div className="overflow-hidden order-1 lg:order-1 relative group">
              <img className="w-full object-cover  transition-all duration-300 group-hover:scale-145 group-hover:rotate-12" src={bgHeading} alt="" />
              <div className="transition-all delay-150 duration-75 ease-in brightness-100 opacity-0 bg-white group-hover:opacity-55 absolute h-full w-full top-0 -skew-x-12 group-hover:translate-x-full">
                <div>&nbsp;</div>
              </div>
              <div className="transition-all delay-150 duration-150 ease-in brightness-200 opacity-0 bg-white group-hover:opacity-55 absolute h-full w-full top-0 -skew-x-12 group-hover:translate-x-full">
                <div>&nbsp;</div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
}

export default HeadingSession;
