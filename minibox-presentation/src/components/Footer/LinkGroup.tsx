export const LinkGroup = ({ children, header }: any) => {
    return (
      <>
        <div className="w-full">
          <div className="w-full">
            <h4 className="mb-9 text-lg font-semibold text-dark">{header}</h4>
            <ul className="space-y-3">{children}</ul>
          </div>
        </div>
      </>
    );
  };