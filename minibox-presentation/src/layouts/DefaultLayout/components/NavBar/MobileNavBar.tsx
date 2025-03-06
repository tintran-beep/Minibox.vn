import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import { ShoppingIcon, NotifyIcon } from "@components/index";
import miniLogo from "@assets/mini_logo.svg";

const MobileNavBar = () => {
  const [keyword, setKeyword] = useState("");
  const hanlderSearch = () => {
    console.log(`Search-${keyword}`);
  };

  return (
    <section className="z-10">
      <nav className="bg-white shadow-lg">
        <div className="flex flex-row text-center items-center py-2 px-0">
          {/* Logo */}
          <div className="flex justify-around px-1 py-1 cursor-pointer flex-none">
            <NavLink to="/">
              <img
                className="block object-cover min-h-14 min-w-14 h-16 w-16"
                src={miniLogo}
              />
            </NavLink>
          </div>
          {/* Menu */}
          <div className="block flex-1">
            <div className="flex justify-end basis-1/4">
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
          </div>

          {/* Actions */}
          <div className="flex justify-evenly px-2 flex-none">
            <ul className="flex items-center gap-6 font-semibold px-2">
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
            </ul>
          </div>
        </div>
      </nav>
    </section>
  );
};

export default MobileNavBar;
