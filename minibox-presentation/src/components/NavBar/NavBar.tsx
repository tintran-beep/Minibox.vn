import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faMagnifyingGlass,
  faUser
} from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import { ShoppingIcon, NotifyIcon } from "@components/index";
import logo from "@assets/logo_web.svg";
import miniLogo from "@assets/mini_logo.svg";

const NavBar = () => {
  const [keyword, setKeyword] = useState("");
  const hanlderSearch = () => {
    console.log(`Search-${keyword}`);
  };

  return (
    <section>
      <nav className="bg-white shadow-lg">
        <div className="flex flex-row justify-around text-center items-center py-2 lg:py-0 px-0 lg:px-3 mix-w-[320px] w-screen">
          {/* Logo */}
          <div className="flex items-center font-semibold px-1 py-1 lg:px-4 lg:py-4 cursor-pointer">
            <NavLink to="/">
              <img
                className="hidden lg:block sm:hidden object-cover min-h-20 min-w-20"
                src={logo}
              />
              <img
                className="block sm:block lg:hidden object-cover min-h-14 min-w-14 h-16 w-16"
                src={miniLogo}
              />
            </NavLink>
          </div>
          {/* Menu */}
          <div className="hidden md:block md:basic-3/4 lg:basis-2/4">
            <ul className="flex flex-row items-center gap-6 font-semibold">
              <li className="inline-block basis-1/10 hover:text-primary">
                <NavLink to="/">Home</NavLink>
              </li>
              <li className="inline-block basis-1/10 hover:text-primary">
                <NavLink to="/">Category</NavLink>
              </li>
              <li className="inline-block  basis-1/10 hover:text-primary">
                <NavLink to="/">Shop</NavLink>
              </li>
              <li className="inline-block  basis-1/10 hover:text-primary">
                <NavLink to="/">Blog</NavLink>
              </li>
              {/* Search */}
              <li className="basis-5/10">
                <div className="flex justify-end min-w-[130px] basis-1/4">
                  <div className="w-full">
                    <label htmlFor="simple-search" className="sr-only">
                      Search
                    </label>
                    <div className="relative w-full">
                      <div className="absolute inset-y-0 start-0 flex items-center ps-3">
                        <button //type="submit"
                          onClick={hanlderSearch}
                        >
                          <FontAwesomeIcon
                            className="w-4 h-4 text-gray-500 dark:text-gray-400 cursor-pointer hover:text-primary"
                            icon={faMagnifyingGlass}
                          />
                        </button>
                      </div>
                      <input
                        type="text"
                        id="simple-search"
                        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full ps-10 p-2.5"
                        placeholder="Search branch name..."
                        value={keyword}
                        onChange={(e) => setKeyword(e.target.value)}
                        required
                      />
                    </div>
                  </div>
                </div>
              </li>
            </ul>
          </div>

          {/* Actions */}
          <div className="flex justify-evenly px-2">
            <ul className="flex items-center gap-4 lg:gap-10 font-semibold px-2">
              <li className="inline-block">
                <NavLink to="/">
                  <ShoppingIcon value={10} />
                </NavLink>
              </li>

              <li className="inline-block">
                <NavLink to="/">
                  <NotifyIcon value={100} />
                </NavLink>
              </li>
              <li className="inline-block">
                <NavLink to="/">
                  <FontAwesomeIcon className="cursor-pointer" icon={faUser} />
                </NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </section>
  );
};

export default NavBar;
