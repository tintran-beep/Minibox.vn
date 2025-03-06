export const NavLink = ({ link, label }: any) => {
    return (
      <li>
        <a
          href={link}
          className="inline-block text-base leading-loose text-body-color hover:text-primary"
        >
          {label}
        </a>
      </li>
    );
  };

  