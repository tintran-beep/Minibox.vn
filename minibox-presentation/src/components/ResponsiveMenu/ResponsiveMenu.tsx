import { motion, AnimatePresence } from "framer-motion";
import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faSignOut } from "@fortawesome/free-solid-svg-icons";

const ResponsiveMenu = ({ open }: { open: boolean }) => {
  return (
    <AnimatePresence mode="wait">
      {open && (
        <motion.div
          initial={{ opacity: 0, y: -100 }}
          animate={{ opacity: 1, y: 0 }}
          exit={{ opacity: 0, y: -100 }}
          transition={{ duration: 0.3 }}
          className="absolute right-0 bottom-20 w-1/3 z-50"
        >
          <div className="text-base pr-2">
            <ul className="flex flex-col gap-4 bg-gray-950 text-white rounded-3xl py-4">
              <li className="cursor-pointer">
                <NavLink to="/">
                  <FontAwesomeIcon className="px-2" icon={faUser} />
                  Profile
                </NavLink>
              </li>
              <li>
              <NavLink to="/">
                  <FontAwesomeIcon className="px-2" icon={faSignOut} />
                  Log out
                </NavLink>
              </li>
            </ul>
          </div>
        </motion.div>
      )}
    </AnimatePresence>
  );
};

export default ResponsiveMenu;
