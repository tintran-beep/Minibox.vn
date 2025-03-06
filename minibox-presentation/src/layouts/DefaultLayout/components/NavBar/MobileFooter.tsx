import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useLocation } from "react-router-dom";
import {
  faBars,
  faHouse,
  faList,
  faBagShopping,
  faPenClip,
} from "@fortawesome/free-solid-svg-icons";
import { routesConstant } from "@configs/routes";
import { NavLink } from "react-router-dom";

const MobileFooter = ({ handlerClickMenu }: any) => {
  const location = useLocation();

  const getActiveClass = (path: string) => {
    return location.pathname === path ? "text-primary" : "";
  };

  return (
    <div className="z-10 mix-w-[320px] w-screen">
      <div className="flex flex-row justify-around text-center items-center ">
        {/* Menu */}
        <div
          className={`cursor-pointer hover:underline hover:text-primary ${getActiveClass(
            routesConstant.home
          )}`}
        >
          <NavLink to="/">
            <FontAwesomeIcon className="px-1" icon={faHouse} /> Home
          </NavLink>
        </div>
        <div
          className={`cursor-pointer hover:underline hover:text-primary ${getActiveClass(
            routesConstant.categories
          )}`}
        >
          <NavLink to="/">
            <FontAwesomeIcon className="px-1" icon={faList} />
            Category
          </NavLink>
        </div>
        <div
          className={`cursor-pointer hover:underline hover:text-primary ${getActiveClass(
            routesConstant.shop
          )}`}
        >
          <FontAwesomeIcon className="px-1" icon={faBagShopping} />
          Shop
        </div>
        <div
          className={`cursor-pointer hover:underline hover:text-primary ${getActiveClass(
            routesConstant.blog
          )}`}
        >
          <FontAwesomeIcon className="px-1" icon={faPenClip} />
          Blog
        </div>
        {/* Mobile menu session */}
        <div
          className="cursor-pointer hover:underline hover:text-primary"
          onClick={handlerClickMenu}
        >
          <FontAwesomeIcon className="text-4xl cursor-pointer" icon={faBars} />
        </div>
      </div>
    </div>
  );
};

export default MobileFooter;
