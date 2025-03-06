import { TCategory } from "@customtypes/category";
import { Link } from "react-router-dom";

const CateCard = ({title, img }: TCategory) => {
  return (
    <Link to={`/products/category/${title}`}>
        <div className="relative group overflow-hidden">
          <div className="overflow-hidden">
            <img
              className="object-cover w-full transition-all duration-300 group-hover:scale-125"
              src={img}
              alt=""
            />
          </div>
          <div className="flex justify-center mt-4 space-x-4">
            <div>
              <h3 className="text-xs font-bold text-gray-900 sm:text-sm md:text-base">
              {title}
              <span className="absolute inset-0" aria-hidden="true"></span>
              </h3>
            </div>
          </div>
        </div>
      </Link>
  );
};

export default CateCard;
