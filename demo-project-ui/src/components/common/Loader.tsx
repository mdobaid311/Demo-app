import { Loader2 } from "lucide-react";

const Loader = () => {
  return (
    <div className="flex items-center justify-center min-h-screen animate-spin">
      <Loader2 />
    </div>
  );
};

export default Loader;
