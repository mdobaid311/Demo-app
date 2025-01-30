import employeesAPI from "@/api/employeeAPI";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { ISalary } from "@/interfaces/ISalary";
import moment from "moment";
import { useEffect, useState } from "react";
import { DeleteIcon } from "../common/icons";

type Props = {
  showSalariesDialog: boolean;
  employeeID: string;
  onClose: () => void;
};

const SalariesDialog = ({ showSalariesDialog, employeeID, onClose }: Props) => {
  const [salaries, setSalaries] = useState<ISalary[]>([]);
  const [newSalary, setNewSalary] = useState<ISalary | null>(null);

  const getSalaries = async () => {
    setSalaries([]);
    employeesAPI.getSalariesbyEmployeeID(employeeID).then((res) => {
      if (res?.data?.salaries) setSalaries(res.data.salaries);
    });
  };

  useEffect(() => {
    if (showSalariesDialog) {
      getSalaries();
    }
  }, [showSalariesDialog, employeeID]);

  const handleAddNewSalaryRow = () => {
    setNewSalary({ month: "", salaryAmount: 0, salaryID: "", employeeID });
  };

  const handleSaveNewSalary = () => {
    if (newSalary?.month && newSalary.salaryAmount > 0) {
      console.log(newSalary);
      const body = {
        employeeID: newSalary.employeeID,
        salaryAmount: newSalary.salaryAmount,
        month: moment(newSalary.month).format("YYYY-MM-DD"),
      };
      employeesAPI.createSalary(body).then((res) => {
        if (res.status === 200) {
          getSalaries();
        }
      });
      setNewSalary(null);
    }
  };

  const deleteSalary = (salaryID: string) => {
    employeesAPI.deleteSalary(salaryID).then((res) => {
      if (res.status === 200) {
        getSalaries();
      }
    });
  };

  return (
    <Dialog open={showSalariesDialog} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-[40vw]">
        <DialogHeader>
          <DialogTitle>Employee Salaries</DialogTitle>
        </DialogHeader>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="w-[100px]">Month</TableHead>
              <TableHead className="text-center">Amount</TableHead>
              <TableHead className="text-center"></TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {salaries.map((salary, index) => (
              <TableRow key={index}>
                <TableCell className="font-medium">
                  {moment(salary.month).format("MMM YYYY")}
                </TableCell>
                <TableCell className="text-center">
                  {salary.salaryAmount}
                </TableCell>
                <TableCell className="w-[100px] text-center">
                  <Button
                    variant="destructive"
                    size="sm"
                    onClick={() => deleteSalary(salary.salaryID)}
                  >
                    <DeleteIcon />
                  </Button>
                </TableCell>
              </TableRow>
            ))}
            {newSalary && (
              <TableRow>
                <TableCell className="font-medium">
                  <input
                    type="month"
                    className="border rounded px-2 py-1 w-full"
                    value={newSalary.month}
                    onChange={(e) =>
                      setNewSalary({ ...newSalary, month: e.target.value })
                    }
                  />
                </TableCell>
                <TableCell className="text-center">
                  <input
                    type="number"
                    className="border rounded px-2 py-1 w-full text-center"
                    value={newSalary.salaryAmount}
                    onChange={(e) =>
                      setNewSalary({
                        ...newSalary,
                        salaryAmount: parseFloat(e.target.value),
                      })
                    }
                  />
                </TableCell>
              </TableRow>
            )}
          </TableBody>
          <TableFooter>
            <TableRow>
              <TableCell>Total</TableCell>
              <TableCell className="text-center">
                {salaries.reduce(
                  (total, salary) => total + salary.salaryAmount,
                  0
                )}
              </TableCell>
            </TableRow>
          </TableFooter>
        </Table>
        <div className="flex flex-col items-center mt-4">
          {!newSalary ? (
            <Button onClick={handleAddNewSalaryRow}>Add New Salary</Button>
          ) : (
            <Button onClick={handleSaveNewSalary} className="mt-2">
              Save Salary
            </Button>
          )}
        </div>
        <DialogFooter>
          <Button onClick={onClose}>Close</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default SalariesDialog;
