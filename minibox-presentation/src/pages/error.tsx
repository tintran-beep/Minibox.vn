import { Link, useRouteError, isRouteErrorResponse } from "react-router-dom";

function Error() {
  const error = useRouteError();
  let errorStatus: number;
  let errorStatusText: string;

  if (isRouteErrorResponse(error)) {
    errorStatus = error.status;
    errorStatusText = error.statusText;
  } else {
    errorStatus = 404;
    errorStatusText = "Page Not Found";
  }
  return (
    <div className="text-center text-4xl m-2">
      <h1>{errorStatus}</h1>
      <p>{errorStatusText}</p>
      <Link to="/" replace={true}>
      <button className="bg-amber-400 rounded-2xl border-2 p-2 text-2xl">
          Go Home
      </button>
      </Link>
    </div>
  );
}

export default Error;
